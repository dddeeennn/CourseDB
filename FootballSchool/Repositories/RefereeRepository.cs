using System.Linq;

namespace FootballSchool.Repositories
{
    public class RefereeRepository : RepositoryBase
    {
        public Referee GetRefereeById(int id)
        {
            return Entities.Referee.FirstOrDefault(x => x.Id == id);
        }

        public string GetRefereeFullName(int id)
        {
            var referee = GetRefereeById(id);
            return referee.LastName + " " + referee.Name;

        }
    }
}
