
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

using System.Threading.Tasks;
using UnityEngine;

public class DatabaseAccess : MonoBehaviour
{
    
    MongoClient client = new MongoClient("mongodb+srv://trrenty:0aehkUKDe09kDIwr@cluster0.kkkib.mongodb.net/FilterApp?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<User> collection;
    // Start is called before the first frame update
    void Start()
    {
        database = client.GetDatabase("FilterApp");
        collection = database.GetCollection<User>("FilterAppUsers");


        //var document = new User { Username = "trrent2", Password = "test", Email = "test@gmail.com"  };
        //collection.InsertOne(document);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async Task<bool> IsUserValid(string username, string password)
    {
        var userQuery = collection.FindAsync(u => u.Username == username && u.Password == password);
        var result = await userQuery;
        Debug.Log(result.ToString());
        if (result.ToList().Count <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    public async Task<bool> InsertUser(string username, string password, string email)
    {
        var userQ = collection.FindAsync(u => u.Username == username);
        var user = await userQ;

        if (user.ToList().Count > 0)
        {
            return false;
        }
        else
        {
            await collection.InsertOneAsync(new User { Email = email, Username = username, Password = password });
            return true;
        }

    }

}



public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}
