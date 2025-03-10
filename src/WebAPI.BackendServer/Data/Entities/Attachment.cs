﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.BackendServer.Data.Entities.Interface;

namespace WebAPI.BackendServer.Data.Entities
{
	[Table("Attachment")]
	public class Attachment : IDateTracking
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[MaxLength(200)]
		public string FileName { get; set; }

		[Required]
		[MaxLength(200)]
		public string FilePath { get; set; }

		[Required]
		[MaxLength(4)]
		[Column(TypeName = "varchar(4)")]
		public string FileType { get; set; }

		[Required]
		public long FileSize { get; set; }
		public int? KnowledgeBaseId { get; set; }
		public int? CommentId { get; set; }

		[Required]
		[MaxLength(200)]
		[Column(TypeName = "varchar(10)")]
		public string Type { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? LastUpdateTime { get; set; }
	}
}
