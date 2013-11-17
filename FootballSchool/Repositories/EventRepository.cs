using System.Linq;

namespace FootballSchool.Repositories
{
	/// <summary>
	/// event repo
	/// </summary>
	public class EventRepository : RepositoryT<Event>
	{
		/// <summary>
		/// Gets the event by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public Event GetEventById(int id)
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
