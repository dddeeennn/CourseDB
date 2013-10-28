using System.Linq;

namespace FootballSchool.Repositories
{
	/// <summary>
	/// refereee repo
	/// </summary>
	public class RefereeRepository : RepositoryBase
	{
		/// <summary>
		/// Gets the referee by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public Referee GetRefereeById(int id)
		{
			return Entities.Referee.FirstOrDefault(x => x.Id == id);
		}

		/// <summary>
		/// Gets the full name of the referee.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public string GetRefereeFullName(int id)
		{
			var referee = GetRefereeById(id);
			return referee.LastName + " " + referee.Name;
		}
	}
}
