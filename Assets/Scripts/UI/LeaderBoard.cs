using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{

    [SerializeField] private RectTransform LeaderItem;
    [SerializeField] private RectTransform Content;

    private List<GameObject> _items;
    
    private List<Saver.User> users = null;
    private void OnEnable()
    {
        users = Saver.instance.GetUserList();
        _items = new List<GameObject>();
        
        for (int i = 0; i < users.Count; i++)
        {
            GameObject m = Instantiate(LeaderItem.gameObject, Content);
            LeaderItem item = m.GetComponent<LeaderItem>();
            item.Name.text = users[i].Name;
            item.Score.text = users[i].Score.ToString();
            item.Place.text = (i+1).ToString();
            _items.Add(m);
        }
    }

    private void OnDisable()
    {
        if(_items != null)
        foreach (var i in _items)
        {
            Destroy(i);
        }
        _items = null;
    }
}
