using System.Linq;

namespace FootballSchool.Repositories
{
	/// <summary>
	/// event repo
	/// </summary>
	public class EventRepository : RepositoryT<Events>
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
		/// Gets  type of event.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public string GetEventType(int id)
		{
			return GetEventById(id).Type;
		}
	}
}
