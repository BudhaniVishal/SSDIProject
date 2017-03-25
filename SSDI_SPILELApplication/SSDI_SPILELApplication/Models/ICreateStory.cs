using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDI_SPILELApplication.Models
{
     interface ICreateStory
    {
        bool CreateEditorStory(StoryModel story);
    }
}
