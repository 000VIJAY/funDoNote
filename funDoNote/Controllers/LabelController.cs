using BusinessLayer.Interfaces;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace funDoNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LabelController : ControllerBase
    {
        private IConfiguration _config;
        private FunDoNoteContext _funDoNoteContext;
        private ILabelBL _labelBL;
        public LabelController(ILabelBL labelBL, IConfiguration config, FunDoNoteContext funDoNoteContext)
        {
            this._funDoNoteContext = funDoNoteContext;
            this._config = config;
            this._labelBL = labelBL;
           
        }
        [Authorize]
        [HttpPost("AddLabelName/{NoteId}/{labelName}")]
        public async Task<IActionResult> AddLabel(int NoteId , string labelName)
        {
            var labelNote =  await _funDoNoteContext.Labels.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
            if (labelNote == null)
            {
                return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist so create a note to add label" });
            }
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);

             await this._labelBL.AddLabel(UserID, NoteId, labelName);
            return this.Ok(new { success = true, status = 200, message = "Label added successfully" });
        }
       
        [Authorize]
        [HttpGet("GetLabelsByNoteId/{NoteId}")]
        public async  Task<IActionResult> GetLabelsByNoteId(int NoteId)
        {
            var labelNote = await _funDoNoteContext.Labels.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
            if (labelNote == null)
            {
                return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist " });
            }
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);

            
            var labels = await this._labelBL.GetLabelsByNoteId(UserID,NoteId);
            return this.Ok(new { success = true, status = 200,Labels = labels});
        }
        [Authorize]
        [HttpGet("GetLabelByNoteIdwithJoin/{NoteId}")]
        public async Task<IActionResult> GetLabelByNoteIdwithJoin(int NoteId)
        {
            var labelNote = await _funDoNoteContext.Labels.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
            if (labelNote == null)
            {
                return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist " });
            }
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);

           
           var labels = await this._labelBL.GetLabelByNoteIdwithJoin(UserID, NoteId);
            return this.Ok(new { success = true, status = 200, Labels = labels });
        }
        [Authorize]
        [HttpGet("GetLabelByUserIdWithJoin")]
        public  async Task<IActionResult> GetLabelByUserIdWithJoin()
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);


            var labels = await this._labelBL.GetLabelByUserIdWithJoin(UserID);
            return this.Ok(new { success = true, status = 200, Labels = labels });
        }

        [Authorize]
        [HttpPut("UpdateLabel/{NoteId}/{newLabel}")]
        public async Task<IActionResult> UpdateLabel(int NoteId ,string newLabel)
        {
            var labelNote = await _funDoNoteContext.Labels.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
            if (labelNote == null)
            {
                return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist " });
            }
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);

            await this._labelBL.UpdateLabel(UserID, NoteId,newLabel);
            return this.Ok(new { success = true, status = 200,message = "Label Updated successfully"});
        }
        [Authorize]
        [HttpDelete("DeleteLabel/{NoteId}")]
        public async Task<IActionResult> DeleteLabel(int NoteId)
        {
            var labelNote = await _funDoNoteContext.Labels.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
            if (labelNote == null)
            {
                return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist " });
            }
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);

            var res =  await this._labelBL.DeleteLabel(UserID, NoteId);
            return this.Ok(new { success = true, status = 200, message = "Label Deleted successfully" });
        }
    }
}
