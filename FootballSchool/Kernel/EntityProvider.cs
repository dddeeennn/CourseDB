namespace FootballSchool.Kernel
{
    public class EntityProvider
    {
        private static readonly fscEntities FscEntities = new fscEntities();

        public static fscEntities Entities
        {
            get { return FscEntities; }
        }

        protected EntityProvider() { }
    }
}
