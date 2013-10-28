using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;

namespace FootballSchool.Repositories
{
	/// <summary>
	/// base class for repositories
	/// </summary>
	public abstract class RepositoryBase
	{
		protected fscEntities Entities { get; set; }

		protected RepositoryBase()
		{
			Entities = new fscEntities();
		}
	}
}
