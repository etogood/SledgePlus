﻿using System.Collections.ObjectModel;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Commands.InnerActions;
using SledgePlus.WPF.ViewModels.UserControls.Custom;

namespace SledgePlus.WPF.ViewModels.UserControls;



public class LearningMenuViewModel : ViewModel
{
    private readonly AppDbContext _appDbContext;

    private ObservableCollection<ExpanderLessonItemViewModel> _sections;

    public ObservableCollection<ExpanderLessonItemViewModel> Sections
    {
        get => _sections;
        set => Set(ref _sections, value);
    }

    public LearningMenuViewModel(IHost host)
    {
        _appDbContext = host.Services.GetRequiredService<AppDbContext>();

        Sections = new ObservableCollection<ExpanderLessonItemViewModel>();

        LabelBuilder labelBuilder = new();

        foreach (var section in _appDbContext.Sections.ToList())
        {
            ObservableCollection<LessonItemViewModel> innerItems = new();

            labelBuilder.FirstNumber += 1;
            labelBuilder.SecondNumber = 0;

            foreach (var lesson in _appDbContext.Lessons.Where(x => x.SectionId == section.SectionId))
            {
                var label = $"{labelBuilder.FirstNumber}.{labelBuilder.SecondNumber+=1} {GetLabelName(lesson)}";
                innerItems.Add(new LessonItemViewModel(host)
                {
                    Label = label,
                    Description = lesson.LessonDescription,
                    Command = new OpenLessonDocument(host, lesson.LessonDocumentName)
                });
            }

            Sections.Add(new ExpanderLessonItemViewModel(host)
            {
                Header = section.SectionHeader,
                InnerItems = innerItems
            });
        }
    }

    private struct LabelBuilder
    {
        public int FirstNumber;
        public int SecondNumber;
        public string Name;
    }

    private string GetLabelName(Lesson lesson) => lesson.IsPractice ? "Практика" : "Лекция";
    
}