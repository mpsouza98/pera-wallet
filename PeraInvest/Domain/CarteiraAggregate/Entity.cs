namespace PeraInvest.Domain.CarteiraAggregate {
    public abstract class Entity {
        byte[] _Id;
        public virtual byte[] Id {
            get {
                return _Id;
            }
            protected set {
                _Id = new Guid().ToByteArray();
            }
        }
    }
}
