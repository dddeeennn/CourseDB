using System.Collections.Generic;
using System.Linq;

namespace FootballSchool.Repositories
{
	/// <summary>
	/// repo for Coach
	/// </summary>
	public class CoachRepository : RepositoryBase
	{
		/// <summary>
		/// Gets all coaches
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Coaches> GetAll()
		{
			return Entities.Coaches;
		}

		/// <summary>
		/// Get coach by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public Coaches GetCoachById(int id)
		{
			return Entities.Coaches.FirstOrDefault(x => x.Id == id);
		}

		/// <summary>
		/// Get full name.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public string GetFullName(int id)
		{
			var coach = GetCoachById(id);
			return coach.LastName + " " + coach.Name;
		}
	}
}
