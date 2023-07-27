namespace Strategy;

public interface INavigationStrategy
{
    Route Navigate(string origin, string destination);
}
