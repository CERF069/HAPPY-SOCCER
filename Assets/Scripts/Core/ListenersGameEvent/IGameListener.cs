public interface IGameListener { }

public interface IStartGameListener: IGameListener { void OnStartGame(); }
public interface IWonGameListener : IGameListener { void OnWonGame(); }
public interface ILoseGameListener: IGameListener { void OnLoseGame(); }
public interface IPauseGameListener: IGameListener { void OnPauseGame(); }
public interface IResumeGameListener: IGameListener { void OnResumeGame(); }
