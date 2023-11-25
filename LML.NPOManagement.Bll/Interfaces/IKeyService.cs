using System;
using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Bll.Interfaces
{
	public interface IKeyService
	{
        Task<KeyModel> GetKey(string recovery);
        Task<KeyModel> GenerateKeys(string email);
    }
}

