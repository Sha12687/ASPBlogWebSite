using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using BlogWebsite.Models.ViewModel;
using BlogWebsite.Data;
using BlogWebsite.Models.Domain;
using Microsoft.EntityFrameworkCore;
using BlogWebsite.Models.Reposotiry;
using Azure;
using Microsoft.AspNetCore.Authorization;

namespace BlogWebsite.Controllers
{
    [Authorize (Roles ="admin")]
    public class AdminTagsController : Controller
    {
        readonly private ITagRepository tagRepository;
        public AdminTagsController(ITagRepository repository)
        {
            this.tagRepository = repository;
        }
        [HttpGet]
        [Authorize (Roles ="Admin")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> SaveTag(TagModel tag)
        {
            Tag tag1 = new Tag
            {
                Name = tag.Name,
                DisplayName = tag.DisplayName
            };
            await tagRepository.AddAsync(tag1);
            return RedirectToAction("Tags");
        }

        [HttpGet]
        [ActionName("Tags")]
        public async Task<IActionResult> ListOfTag()
        {
            IEnumerable<Tag> tagList = await tagRepository.getAllAsync();
            return View(tagList);
        }

        [HttpGet]
        public async Task<ActionResult> EditTag(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);
            TagEditModel tagEdit = new TagEditModel();
            if (tag != null)
            {
                tagEdit.Name = tag.Name;
                tagEdit.DisplayName = tag.DisplayName;
                return View(tagEdit);
            }
            return View(null);
        }


        [HttpPost]
        public async Task<IActionResult> EditTag(TagEditModel editModel)
        {
            Tag tag1 = new Tag
            {
                Id=editModel.Id,
                Name = editModel.Name,
                DisplayName = editModel.DisplayName
            };

       var updatedTag=await tagRepository.UpdateAsync(tag1);
            if(updatedTag != null)
            {
                // success notification
            }
            else
            {
                // fail notification 
            }

            return RedirectToAction("Tags");
        }

        [HttpPost]
          public async Task<ActionResult> DeleteTag(TagEditModel editModel)
        {
       var deletedtag =  await tagRepository.DeleteAsync(editModel.Id);
            if (deletedtag != null)
            {
                // success notification
                return RedirectToAction("Tags");
            }
            else
            {
                // fail notification 
                return View("Edit", new { editModel.Id });
            }
        }

     
    }

   

}
