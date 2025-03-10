﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.BackendServer.Data.Entities;

namespace WebAPI.BackendServer.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
			builder.Entity<User>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
			builder.Entity<LabeInlKnowledgeBase>().HasKey(c => new { c.LabelId, c.KnowledBaseId });
			builder.Entity<Permission>().HasKey(c => new { c.RoleId, c.FunctionId, c.CommandId });
			builder.Entity<Vote>().HasKey(c => new { c.KnowledBaseId, c.UserId });
			builder.Entity<CommandInFunctions>().HasKey(c => new { c.CommandId, c.FunctionId });
			builder.HasSequence("KnowledgeBaseSequence");
		}
		public DbSet<Command> Commands { set; get; }
		public DbSet<CommandInFunctions> CommandInFunctions { set; get; }
		public DbSet<ActivityLogs> ActivityLogs { set; get; }
		public DbSet<Category> Categories { set; get; }
		public DbSet<Comment> Comments { set; get; }
		public DbSet<Function> Functions { set; get; }
		public DbSet<KnowledgeBase> KnowledgeBases { set; get; }
		public DbSet<Label> Labels { set; get; }
		public DbSet<LabeInlKnowledgeBase> LabelInKnowledgeBases { set; get; }
		public DbSet<Permission> Permissions { set; get; }
		public DbSet<Report> Reports { set; get; }
		public DbSet<Vote> Votes { set; get; }
		public DbSet<Attachment> Attachments { set; get; }

	}
}