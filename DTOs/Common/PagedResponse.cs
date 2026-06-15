namespace TraineeManagement.api.DTOs;
using System;
using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.Models;


public class PagedResponse<T>
{
    public int PageNumber{get;set;}
    public int PageSize {get;set;}

    public int TotalRecords {get; set;} 
    
    public List<T> Data {get; set;}

    public PagedResponse(List<T> data,int pageno, int pgsz, int totalrec){
        PageNumber = pageno;
        PageSize = pgsz;
        TotalRecords = totalrec;
        Data = data;

    }
}