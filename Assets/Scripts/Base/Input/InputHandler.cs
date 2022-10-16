
namespace Base.Input
{
    public abstract class InputHandler
    {
        public InputHandlerType Type { get; private set; }

        public bool IsDisabled { get; set; }

        protected InputHandler(InputHandlerType type)
        {
            Type = type;
        }

        public virtual void OnKeyDown(InputInfo inputInfo) { }

        public virtual void OnClick(InputInfo inputInfo) { }

        public virtual void OnMouseMove(InputInfo inputInfo) { }
    }
}
