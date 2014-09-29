﻿namespace SnakeMess
{
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

    /// <summary>
    /// The snake component.
    /// </summary>
    public class SnakeComponent : Component
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnakeComponent"/> class.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        public SnakeComponent(Vector position, SnakePart type) : base(position)
        {
            Type = type;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        private SnakePart Type { get; set; }
    }
}