using System;

namespace OrderingSystem.Shared.Entities
{
  public abstract class Entity
  {
    public bool Active { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
  }
}