using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Saver : MonoBehaviour
{
    public static Saver instance = null;
    private string _save;
    public int PlayerScore;
    public string PlayerName;
    
    List<User> _allUsers = new List<User>();
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if(PlayerPrefs.HasKey("Save"))
        _save = PlayerPrefs.GetString("Save");
        else
        {
            PlayerPrefs.SetString("Save", "AAAAA_0");

        }
        Debug.Log(_save);
    }
    
    public List<User> GetUserList()
    {
        List<User> allUsers = new List<User>();
        _save = PlayerPrefs.GetString("Save");
        string[] users = _save.Split('|');
        
        for (int i = 0; i < users.Length; i++)
        {
            string[] user = users[i].Split('_');
            User New = new User();
            New.Name = user[0];
            New.Score = int.Parse(user[1]);
            allUsers.Add(New);
        }
        return allUsers;
    }
    
    private void SaveUser(User user)
    {
        List<User> temp ;
        if (GetUserList() != null)
        {
            temp = GetUserList();
        }
        else
        {
            temp = new List<User>();
        }
        temp.Add(user);
        temp.Sort(delegate(User us1, User us2)
            { return us1.Score.CompareTo(us2.Score); });

        temp.Reverse();
        _save = "";
        
        for (int i = 0; i < temp.Count; i++)
        {
            if (i < temp.Count - 1)
            {
                _save += temp[i].Name + '_' + temp[i].Score + '|';
            }
            else
            {
                _save += temp[i].Name + '_' + temp[i].Score;
            }
        }
        PlayerPrefs.SetString("Save", _save);
        
    }

    public void SaveUser()
    {
        User u = new User(){Name = PlayerName, Score = PlayerScore};
        SaveUser(u);
    }
    
    
    [Serializable]
    public class User
    {
        public string Name;
        public int Score;
    }
}
