
namespace Nexus.Engine {

	public interface IEmitter {
		bool HasExpired { get; }
		void ReturnEmitter();
		void RunEmitterTick();
		bool IsOnScreen(Camera camera);
		void Draw(int camX, int camY);
	}
}
