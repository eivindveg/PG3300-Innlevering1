﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Board
{
	private List<Apple> apples
	{
		get;
		set;
	}

	private List<Snake> snakes
	{
		get;
		set;
	}

	private Vector dimension
	{
		get;
		set;
	}

    public Board(Vector dimension, List<Snake> snakes)
    {
        this.dimension = dimension;
        this.snakes = snakes;
    }

}

