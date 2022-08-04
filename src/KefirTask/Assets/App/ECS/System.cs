using System;

namespace App.ECS
{
    public abstract class System
    {
        public abstract Type[] Filters { get; }

        public void Execute(Entity[] entities)
        {
            foreach (var e in entities)
                Execute(e);
        }

        protected abstract void Execute(Entity entity);
    }
}