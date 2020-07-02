using Microsoft.AspNetCore.Components;
using ReviewApp.Domain.Views;

namespace ReviewApp.Pages
{
    public abstract class PageBase : ComponentBase
    {
        protected string ErrorMessage;
        protected bool HasError;

        protected string ModalErrorMessage;
        protected bool HasModalError;
        
        protected bool DisplayModal;
        protected bool ModifyModal;
        protected bool DeleteModal;

        protected DeleteView DeleteView;

        protected void ShowModal()
        {
            DisplayModal = true;
            ModifyModal = false;
            HideModalError();
        }

        protected void HideModal()
        {
            DisplayModal = false;
            ModifyModal = false;
            HideError();
            HideModalError();
        }

        protected void DisplayDeleteModal()
        {
            DeleteModal = true;
            HideModalError();
        }

        protected void HideDeleteModal()
        {
            DeleteModal = false;
            HideModalError();
        }
        
        protected void DisplayError(string error)
        {
            HasError = true;
            ErrorMessage = error;
        }

        protected void HideError()
        {
            HasError = false;
            ErrorMessage = "";
        }

        protected void DisplayModalError(string error)
        {
            HasModalError = true;
            ModalErrorMessage = error;
        }

        protected void HideModalError()
        {
            HasModalError = false;
            ModalErrorMessage = "";
        }

        protected void GetDeleteInformation(long id, string name)
        {
            DeleteView = new DeleteView(id, name);
            DisplayDeleteModal();
        }
        
        
    }
}