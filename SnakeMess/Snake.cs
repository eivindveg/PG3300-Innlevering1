﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Snake : ICollideable
{
	public List<SnakeComponent> components
	{
		get;
		set;
	}

    public Player player { get; set; }

	private Direction direction
	{
		get;
		set;
	}

	public virtual void move()
	{
		throw new System.NotImplementedException();
	}

	public virtual Vector getHeadLocation()
	{
		throw new System.NotImplementedException();
	}

    public Snake()
    {
    }

    public bool IsInPosition(Vector position)
    {
        foreach (var component in components)
        {
            
        }
    }
}

