using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS_App.Entities;
using NS_App.Models;
using NS_App.Services.Interfaces;

namespace NS_App.Controllers;
[Authorize]
public class FileController : Controller
{

    private readonly IBlobStorageService _blobStorage;
    private readonly ICosmosDBService _cosmosDBService;
    public FileController(IBlobStorageService blobStorage, ICosmosDBService cosmosDBService)
    {

        _blobStorage = blobStorage;
        _cosmosDBService = cosmosDBService;
    }
    public async Task<IActionResult> Index()
    {

        return View(await _cosmosDBService.GetAsync());
    }



    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile files, string description)
    {
        if (files == null)
        {
            return RedirectToAction("Index");
        }
        if (description.Length == 0)
        {

            return RedirectToAction("Index");
        }
        Metadata item = new Metadata()
        {

            Id = Guid.NewGuid().ToString(),
            Name = files.FileName,
            Description = description,
            Size = files.Length,
            CreationDate = DateTime.Now

        };
        await _blobStorage.UploadFileAsync(files, item.Id);
        await _cosmosDBService.AddAsync(item);


        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Download(string fileName, string id)
    {
        string blobName = fileName + id;
        DownloadViewModel downloadedFile = await _blobStorage.DownloadFileAsync(blobName);
        return File(downloadedFile.blobStream, downloadedFile.ContentType, fileName);
    }
    public async Task<IActionResult> Delete(string fileName, string id)
    {
        string blobName = fileName + id;
        await _blobStorage.DeleteFileAsync(blobName);
        await _cosmosDBService.DeleteAsync(id);


        return RedirectToAction("Index");
    }

}

