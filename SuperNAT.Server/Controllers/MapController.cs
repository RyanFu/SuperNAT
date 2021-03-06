﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SuperNAT.Common;
using SuperNAT.Bll;
using SuperNAT.Model;

namespace SuperNAT.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : BaseController
    {
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Map model)
        {
            var rst = new ReturnResult<bool>();

            var bll = new MapBll();
            if (model.id == 0)
            {
                rst = bll.Add(model);
                if (rst.Result)
                {
                    ServerHanlder.ChangeMap((int)ChangeMapType.新增, model);
                }
            }
            else
            {
                var map = bll.GetOne(model);
                rst = bll.Update(model);
                if (rst.Result)
                {
                    if (model.client_id != map.Data.client_id)
                    {
                        ServerHanlder.ChangeMap((int)ChangeMapType.新增, model);
                        ServerHanlder.ChangeMap((int)ChangeMapType.删除, map.Data);
                    }
                    else
                    {
                        ServerHanlder.ChangeMap((int)ChangeMapType.修改, model);
                    }
                }
            }

            return Json(rst);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(Map model)
        {
            var bll = new MapBll();
            var rst = bll.Delete(model);
            if (rst.Result)
            {
                ServerHanlder.ChangeMap((int)ChangeMapType.删除, model);
            }

            return Json(rst);
        }

        [HttpPost]
        [Route("GetOne")]
        public IActionResult GetOne(Map model)
        {
            if (model.id == 0)
            {
                var defalut = new ReturnResult<Map>()
                {
                    Result = true,
                    Data = new Map() { remote = GlobalConfig.DefaultUrl }
                };
                return Json(defalut);
            }
            var bll = new MapBll();
            var rst = bll.GetOne(model);
            return Json(rst);
        }

        [HttpPost]
        [Route("GetList")]
        public IActionResult GetList(Map model)
        {
            var bll = new MapBll();
            var rst = bll.GetList(model);
            return Json(rst);
        }

        [HttpPost]
        [Route("GetMapList")]
        public IActionResult GetMapList(string secret)
        {
            var bll = new MapBll();
            var rst = bll.GetMapList(secret);
            return Json(rst);
        }
    }
}
