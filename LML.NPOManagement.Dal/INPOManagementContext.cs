﻿using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Dal
{
    public interface INPOManagementContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<AccountProgress> AccountProgresses { get; set; } 
        DbSet<Attachment> Attachments { get; set; } 
        DbSet<DailySchedule> DailySchedules { get; set; } 
        DbSet<Donation> Donations { get; set; } 
        DbSet<InventoryType> InventoryTypes { get; set; } 
        DbSet<InvestorInformation> InvestorInformations { get; set; } 
        DbSet<InvestorTierType> InvestorTierTypes { get; set; } 
        DbSet<MeetingSchedule> MeetingSchedules { get; set; } 
        DbSet<Notification> Notifications { get; set; } 
        DbSet<NotificationTransportType> NotificationTransportTypes { get; set; }
        DbSet<NotificationType> NotificationTypes { get; set; } 
        DbSet<Role> Roles { get; set; } 
        DbSet<Template> Templates { get; set; } 
        DbSet<TemplateType> TemplateTypes { get; set; } 
        DbSet<User> Users { get; set; } 
        DbSet<UserIdea> UserIdeas { get; set; } 
        DbSet<UserInformation> UserInformations { get; set; } 
        DbSet<UserInventory> UserInventories { get; set; } 
        DbSet<UserType> UserTypes { get; set; } 
        DbSet<WeeklySchedule> WeeklySchedules { get; set; }
        void SaveChanges();
        Task<int> SaveChangesAsync();
    }
}