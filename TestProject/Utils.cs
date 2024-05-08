using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class Utils
    {
        private static Mock<DbSet<T>> DbSetMock<T>(List<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IAsyncEnumerable<T>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<T>(queryable.GetEnumerator()));

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return dbSetMock;
        }

        public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public TestAsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public ValueTask<bool> MoveNextAsync()
            {
                return new ValueTask<bool>(_inner.MoveNext());
            }

            public T Current => _inner.Current;

            public async ValueTask DisposeAsync()
            {
                await Task.CompletedTask; // Dispose method is synchronous
                _inner.Dispose();
            }
        }

        public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
        {
            private readonly IQueryProvider _inner;

            public TestAsyncQueryProvider(IQueryProvider inner)
            {
                _inner = inner;
            }

            public IQueryable CreateQuery(Expression expression)
            {
                return new TestAsyncEnumerable<TEntity>(expression);
            }

            public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            {
                return new TestAsyncEnumerable<TElement>(expression);
            }

            public object Execute(Expression expression)
            {
                return _inner.Execute(expression);
            }

            public TResult Execute<TResult>(Expression expression)
            {
                return _inner.Execute<TResult>(expression);
            }

            public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
            {
                return new TestAsyncEnumerable<TResult>(expression);
            }

            public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
            {
                return Execute<TResult>(expression);
            }
        }

        public class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
        {
            public TestAsyncEnumerable(IEnumerable<T> enumerable)
                : base(enumerable)
            { }

            public TestAsyncEnumerable(Expression expression)
                : base(expression)
            { }

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
            }

            IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);
        }
    }
}
