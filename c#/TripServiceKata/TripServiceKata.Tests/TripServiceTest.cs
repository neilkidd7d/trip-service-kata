using NUnit.Framework;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
	[TestFixture]
    public class TripServiceTest : ILoggedUserFinder
	{
		[Test]
		public void UserWithoutFriendsHasNoTrips()
		{
			var tripService = new TripService(this);
			var trips = tripService.GetTripsByUser(new User.User());
			Assert.That(trips.Count, Is.EqualTo(0));
		}

		public User.User GetUser()
		{
			return new User.User();
		}
	}
}
