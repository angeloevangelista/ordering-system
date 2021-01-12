using System;
using Flunt.Notifications;

namespace OrderingSystem.Shared.Entities
{
  public abstract class Entity: Notifiable
  {
    public Entity()
    {
      Id = new Guid();
      Active = true;
      CreatedAt = DateTime.Now;
      UpdatedAt = DateTime.Now;
    }

    public Guid Id { get; private set; }
    public bool Active { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    protected void SetUpdatedAt() => UpdatedAt = DateTime.Now;

    public virtual T Activate<T>() where T : Entity
    {
      SetUpdatedAt();
      Active = true;

      return this as T;
    }
    public virtual T Deactivate<T>() where T : Entity
    {
      SetUpdatedAt();
      Active = false;

      return this as T;
    }
  }
}