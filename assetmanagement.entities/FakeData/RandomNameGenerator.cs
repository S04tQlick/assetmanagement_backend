using Bogus;

namespace AssetManagement.Entities.FakeData;

public abstract class FakeDataHelper
{
    private static readonly Faker Faker = new();

    // ✅ Random string
    public static string CompanyName() => FakeDataHelper.Faker.Company.CompanyName();

    // ✅ Random string
    public static string RandomString(int length = 8) =>
        FakeDataHelper.Faker.Random.String(length);

    // ✅ Person data
    public static string FullName() => FakeDataHelper.Faker.Name.FullName();
    public static string FirstName() => FakeDataHelper.Faker.Name.FirstName();
    public static string LastName() => FakeDataHelper.Faker.Name.LastName();
    public static string Email() => FakeDataHelper.Faker.Internet.Email();
    public static string ContactNumber() => Faker.Phone.PhoneNumber();

    // ✅ Location data
    public static string Country() => FakeDataHelper.Faker.Address.Country();
    public static string City() => FakeDataHelper.Faker.Address.City();
    public static string Region() => FakeDataHelper.Faker.Address.State();
    public static string Address() => FakeDataHelper.Faker.Address.FullAddress();
    public static string ZipCode() => FakeDataHelper.Faker.Address.ZipCode();

    // ✅ Dates
    public static DateTime FutureIndefiniteDate() => DateTime.MaxValue;

    public static DateTime FutureDate(int years = 1) =>
        FakeDataHelper.Faker.Date.Future(years, DateTime.UtcNow);

    public static DateTime PastDate(int years = 1) =>
        FakeDataHelper.Faker.Date.Past(years, DateTime.UtcNow);


    // ✅ Coordinates
    public static double Latitude() => FakeDataHelper.Faker.Address.Latitude();
    public static double Longitude() => FakeDataHelper.Faker.Address.Longitude();

    // ✅ Logo URL
    public static string LogoUrl() => FakeDataHelper.Faker.Internet.Avatar();
}