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

	public Vector dimension
	{
		get;
		private set;
	}

    public Board(Vector dimension, List<Snake> snakes)
    {
        this.dimension = dimension;
        this.snakes = snakes;
        PositionSnakes();
    }

    private void PositionSnakes()
    {
        foreach (var snake in snakes)
        {
            Vector position = GetPositionFor(snake.player.id);
            Component head = snake.components.First();
            head.position = position;
        }
    }

    private Vector GetPositionFor(int id)
    {
        if (id <= 0 || id > 4)
        {
            throw new Exception("Unsupported number of players!");
        }
        Vector position = new Vector();
        switch (id)
        {
            case 1:
                position.x = this.dimension.x/4;
                position.y = this.dimension.y/4;
                break;
            case 2:
                position.x = this.dimension.x - (this.dimension.x/4);
                position.y = this.dimension.y/4;
                break;
            case 3:
                position.x = this.dimension.x/4;
                position.y = this.dimension.y - (this.dimension.y/4);
                break;
            case 4:
                position.x = this.dimension.x - (this.dimension.x/4);
                position.y = this.dimension.y - (this.dimension.y/4);
                break;
        }
        return position;
    }
}

