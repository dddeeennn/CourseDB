namespace FootballSchool.Repositories
{
    public class GameEventRepository : RepositoryT<GameEvent>
    {
        public void RemoveByGameId(int gameId)
        {
            var toDeleteList = GetAll(x => x.GameID == gameId);
            foreach (var gameEvent in toDeleteList)
            {
                Entities.GameEvents.DeleteObject(gameEvent);
            }
            Entities.SaveChanges();
        }

        public void Remove(int gameEventId)
        {
            Entities.GameEvents.DeleteObject(GetSingle(x=>x.Id==gameEventId));
            Entities.SaveChanges();
        }
    }
}
