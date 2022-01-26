<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
  public class CourseForUpdateDto
  {
    public string Title { get; set; }
    public string Description { get; set; }
  }
}
=======
﻿namespace CourseLibrary.API.Models
{
    public class CourseForUpdateDto : CourseForManipulationDto
    {
    }
}
>>>>>>> d752139fab204ce7bae20103a059408461d60106
