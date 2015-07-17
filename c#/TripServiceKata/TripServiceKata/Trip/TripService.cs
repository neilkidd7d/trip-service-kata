using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
		private readonly ILoggedUserFinder _loggedUserFinder;

	    public TripService() :
			this(new LoggedUserFinder())
	    {
	    }

		public TripService(ILoggedUserFinder loggedUserFinder)
	    {
		    _loggedUserFinder = loggedUserFinder;
	    }

	    public List<Trip> GetTripsByUser(User.User user)
        {
            List<Trip> tripList = new List<Trip>();
			User.User loggedUser = _loggedUserFinder.GetUser();
            bool isFriend = false;
            if (loggedUser != null)
            {
                foreach(User.User friend in user.GetFriends())
                {
                    if (friend.Equals(loggedUser))
                    {
                        isFriend = true;
                        break;
                    }
                }
                if (isFriend)
                {
                    tripList = TripDAO.FindTripsByUser(user);
                }
                return tripList;
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }
    }

	public interface ILoggedUserFinder
	{
		User.User GetUser();
	}

	public class LoggedUserFinder : ILoggedUserFinder
	{
		public User.User GetUser()
		{
			return UserSession.GetInstance().GetLoggedUser();
		}
	}
}
