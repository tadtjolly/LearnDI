using LearnDI.Interface;
using LearnDI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebDi.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiController : ControllerBase
    {
        private readonly IDiService _diService;
        public DiController(IDiService diService)
        {
            _diService = diService;
        }

        [HttpPost]
        public string GetTestDi()
        {
            var t = 281293;
            var time = _diService.returnDataDi();
            return time;
        }

        int[] sortArray(int[] ar)
        {
            for(int i = 0; i< ar.Length; i++)
            {
                for(int j = i+1; j< ar.Length; j++)
                {
                    if(ar[i] > ar[j])
                    {
                        var temp = ar[i];
                        ar[i] = ar[j];
                        ar[j] = temp;
                    }
                }
            }
            return ar;
        }

        int countSocks(int[] ar)
        {
            var sock = ar[0];
            var dem = 0;
            var resultSock = 0;
            for (int i = 0;i <ar.Length; i++)
            {
                if(ar[i].Equals(sock))
                {
                    dem++;
                    if(i == ar.Length -1)
                    {
                        resultSock += dem / 2;
                    }
                } else
                {
                    resultSock += dem/2;
                    sock = ar[i];
                    dem = 1;
                    continue;
                }
            }
            return resultSock;
        }

        [HttpPost]
        public int sockMerchant(int n, int[] ar)
        {
            var arrayStock = sortArray(ar);
            var result = countSocks(arrayStock);
            return result;
        }

        int countDownHill(string ar)
        {
            var dem = 0;
            var countValley = 0;
            var flag = false;
            for(var i = 0; i< ar.Length; i++)
            {
                var temp = ar.Substring(i,1);
                if(temp.ToString() == "D")
                {
                    dem--;
                    if(dem < 0 && flag == false)
                    {
                        countValley++;
                        flag = true;
                    }
                } else
                {
                    dem++;
                    if (dem >= 0)
                    {
                        flag = false;
                    }
                }
            }
            return countValley;
        } 

        [HttpPost]
        public int climbMoutain(string ar)
        {
            var countValley = countDownHill(ar);
            return countValley;
        }

        int checkCloudNextSave(int[] ar)
        {
            var walkSave = 0;
            if (ar.Length > 2)
            {
                if(ar[0] == 0)
                {
                    var start = ar[0];
                    for (var i = 0; i < ar.Length; i++)
                    {
                        var ruleSteps = 0;
                        var condition = 0;
                        var flag = true;
                        var tempI = i;
                        while(ruleSteps < 2)
                        {
                            if(ar[tempI] == 0 && ruleSteps > 0)
                            {
                                i++;
                            }
                            tempI++;
                            ruleSteps++;
                        }
                    }
                }
            }
            return walkSave;
        }

        [HttpPost]
        public int jumpingOnClouds(int[] ar)
        {
            var t = checkCloudNextSave(ar);
            return t;
        }
    }
}