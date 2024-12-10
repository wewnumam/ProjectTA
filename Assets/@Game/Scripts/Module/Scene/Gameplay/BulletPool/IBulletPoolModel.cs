using Agate.MVC.Base;
using ProjectTA.Module.GameConstants;

namespace ProjectTA.Module.BulletPool
{
    public interface IBulletPoolModel : IBaseModel
    {
        public bool IsPlaying { get; }
        public bool IsShooting { get; }
        public ShootingConstants ShootingConstants { get; }
    }
}