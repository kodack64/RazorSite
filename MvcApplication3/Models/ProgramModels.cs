using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MvcApplication3.Models
{
	public enum Purchasable {
		hoge,
		piyo
	}
	public enum ReferenceVariable {
		alpha,
		beta
	}
	public enum ReferenceOperator {
		greaterThan,
		lessThan,
		equal
	}

	public class ProgramDbContext : DbContext {
		public ProgramDbContext()
			: base("ProgramDbConnection") {
		}

		public DbSet<ProgramModelProfile> ProgramModels { get; set; }
	}

	[Table("ProgramModelProfile")]
	public class ProgramModelProfile {
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public int ProgramId { get; set; }

		[Required]
		[Display(Name = "プログラム名")]
		public string ProgramName { get; set; }

		[Required]
		[Display(Name = "所有者")]
		public string OwnerName { get; set; }

		[Display(Name = "プログラム")]
		public string Program { get; set; }
	}


	public class ProgramModel {
		[Required]
		[Display(Name = "プログラム名")]
		public string UserName { get; set; }

		[Required]
		[Display(Name = "プログラム")]
		public List< Dictionary<ConditionModel , TargetModel> > strategy { get; set; }
	}
	public class ConditionModel {
		[Required]
		[Display(Name = "条件")]
		public List< EquationModel > condition{get;set;}
	}
	public class TargetModel{
		[Required]
		[Display(Name = "購入対象")]
		public Purchasable target {get;set;}
	}
	public class EquationModel{
		[Required]
		[Display(Name = "左辺")]
		public ReferenceVariable leftTerm{get;set;}

		[Required]
		[Display(Name = "右辺")]
		public ReferenceVariable rightTerm{get;set;}

		[Required]
		[Display(Name = "演算子")]
		public ReferenceOperator oeprator{get;set;}
	}
}
