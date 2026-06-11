namespace TraineeManagement.api.DTOs;
using System;
using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.Models;


public class PagedResponse
{
    public int PageNumber{get;set;}
    public int PageSize {get;set;}

    public int TotalRecords {get; set;} 
    
    public List<TraineeResponse>? Data {get; set;}
}