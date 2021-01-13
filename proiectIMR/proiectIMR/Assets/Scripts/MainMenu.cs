
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private DatabaseAccess database;
    void Start()
    {
        database = GameObject.FindGameObjectWithTag("DatabaseAccess").GetComponent<DatabaseAccess>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void Login()
    {

        InputField user = GameObject.FindGameObjectWithTag("user").GetComponent<InputField>();
        InputField pass = GameObject.FindGameObjectWithTag("password").GetComponent<InputField>();
        Debug.Log(user.text);
        Debug.Log(pass.text);

        string passString = md5(pass.text);
        bool task = await database.IsUserValid(user.text, passString);

        if (task)
        {
            LoadCameraScene();
            Debug.Log("Logged In Successfully!");
        }
        else
        {
            Debug.Log("Log In Failed!");
            user.text = "Log In Failed!";
        }
    }

    public void LoadSignUp()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadCameraScene()
    {
        SceneManager.LoadScene(2);
    }

    public async void SignUp()
    {
        InputField user = GameObject.FindGameObjectWithTag("user").GetComponent<InputField>();
        InputField pass = GameObject.FindGameObjectWithTag("password").GetComponent<InputField>();
        InputField email = GameObject.FindGameObjectWithTag("email").GetComponent<InputField>();

        string passString = md5(pass.text);
        bool task = await database.InsertUser(user.text, passString, email.text);

        if (task)
        {
            Debug.Log("Signed In Succesfully");
            user.text = "Signed In Successfully!";
            Invoke("LoadMainMenu", 2);
        }
        else
        {
            Debug.Log("User already exists!");
            user.text = "User already exists!";
        }


    }

    public static string md5(string str)
    {
        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        byte[] bytes = encoding.GetBytes(str);
        var sha = new System.Security.Cryptography.MD5CryptoServiceProvider();
        return System.BitConverter.ToString(sha.ComputeHash(bytes));
    }
}
