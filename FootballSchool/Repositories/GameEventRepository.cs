using System.Linq;

namespace FootballSchool.Repositories
{
	/// <summary>
	/// event repo
	/// </summary>
	public class EventRepository : RepositoryBase
	{
		/// <summary>
		/// Gets the event by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public Events GetEventById(int id)
		{
			return Entities.Events.FirstOrDefault(x => x.Id == id);
		}

		/// <summary>
		/// Gets the type of the even.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public string GetEvenType(int id)
		{
			return GetEventById(id).Type;
		}
	}
}
