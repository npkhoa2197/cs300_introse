using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishContent{
	public string dishname = "Dish Name";
	public Sprite image = null;
	public float score = 0.7f;
    public string description = "Lorem ipsum dolor sit amet, sapien etiam, nunc amet dolor ac odio mauris justo. Luctus arcu, urna praesent at id quisque ac. Arcu es massa vestibulum malesuada, integer vivamus elit eu mauris eus, cum eros quis aliquam wisi.";
    public List<string> options = new List<string> { "Option 1", "Option 2", "Option 3", "Option 4", "Option 5" };
	public float price = 100;
	public int amount = 1;
    public string additionalinfo = "";
    public List<Tuple<string,string> > comments = new List<Tuple<string, string>> { new Tuple<string, string>("User1", "Delicious!"), new Tuple<string, string>("User2", "Brilliant!"), new Tuple<string, string>("User3", "Creative!!") };

    public DishContent()
    {
    }

    //_________________________________________________________________________________________________

    public DishContent(string _dishname, Sprite _image, float _score, string _description, List<string> _options, float _price, int _amount, string _additionalinfo, List<Tuple<string,string> > _comments){
    	dishname = _dishname;
    	image = _image;
    	score = _score;
    	description = _description;
    	options = _options;
    	price = _price;
    	amount = _amount;
    	additionalinfo = _additionalinfo;
    	comments = _comments;
    }
}
