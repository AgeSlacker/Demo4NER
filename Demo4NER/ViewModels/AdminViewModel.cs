using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo4NER.Models;
using Demo4NER.Services;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        private Tag _tagInput = new Tag();
        private Category _categoryInput = new Category();
        private string _message;
        private Color _messageColor;

        public Tag TagInput
        {
            get => _tagInput;
            set => SetProperty(ref _tagInput, value);
        }

        public Category CategoryInput
        {
            get => _categoryInput;
            set => SetProperty(ref _categoryInput, value);
        }

        public String Message
        {
            get => _message;
            set => SetProperty(ref _message,value);
        }

        public Color MessageColor
        {
            get => _messageColor;
            set => SetProperty(ref _messageColor,value);
        }

        public Command AddTagCommand { get; set; }
        public Command DeleteTagCommand { get; set; }
        public Command AddCategoryCommand { get; set; }
        public Command DeleteCategoryCommand { get; set; }
        public AdminViewModel()
        {
            AddTagCommand = new Command(async () => await AddTag());
            DeleteTagCommand = new Command(async () => await DeleteTag());
            AddCategoryCommand = new Command(async () => await AddCategory());
            DeleteCategoryCommand = new Command(async () => await DeleteCategory());
        }

        private async Task DeleteCategory()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            await Task.Run(() =>
            {
                using (var db = new NerContext())
                {
                    var category = db.Categories.FirstOrDefault(c => c.Value == CategoryInput.Value);
                    if (category == null)
                    {
                        Message = $"Não existe categoria {CategoryInput.Value} you dumbfuck.";
                        MessageColor = Color.Red;
                        return;
                    };
                    var result = db.Categories.Remove(category);
                    db.SaveChanges();

                    Message = $"Removido categoria {CategoryInput.Value} com sucesso.";
                    MessageColor = Color.Green;
                }
            });

            CategoryInput = new Category();
            IsBusy = false;
        }

        private async Task AddCategory()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await Task.Run(() =>
            {
                using (var db = new NerContext())
                {
                    db.Add(CategoryInput);
                    db.SaveChanges();
                }
            });

            Message = $"Adicionado categoria {CategoryInput.Value} com sucesso";
            MessageColor = Color.Green;

            CategoryInput = new Category();
            IsBusy = false;
        }

        private async Task DeleteTag()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            await Task.Run(() =>
            {
                using (var db = new NerContext())
                {
                    var tag = db.Tags.FirstOrDefault(t => t.Value == TagInput.Value);
                    if (tag == null)
                    {
                        Message = $"Não existe tag {TagInput.Value} you dumbfuck.";
                        MessageColor = Color.Red;
                        return;
                    };
                    db.Tags.Remove(tag);
                    db.SaveChanges();

                    Message = $"Removido tag {TagInput.Value} com sucesso.";
                    MessageColor = Color.Green;
                }
            });

            TagInput = new Tag();

            BaseBusinessListViewModel.UpdateTagsCommand.Execute(null);

            IsBusy = false;
        }

        private async Task AddTag()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            await Task.Run(() =>
                {
                    using (var db = new NerContext())
                    {
                        db.Tags.Add(TagInput);
                        db.SaveChanges();
                    }
                });

            Message = $"Adicionado tag {TagInput.Value} com sucesso";
            MessageColor = Color.Green;

            TagInput = new Tag();

            BaseBusinessListViewModel.UpdateTagsCommand.Execute(null);

            IsBusy = false;
        }
    }
}
