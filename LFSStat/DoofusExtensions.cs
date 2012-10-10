using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FileHelpers;

namespace LFSStat
{
    partial class exportstats
    {
        public static void csvAllResult(System.Collections.Hashtable raceStat, string datFile, infoRace currInfoRace)
        {

            System.Collections.ArrayList sorted = new System.Collections.ArrayList();
            System.Collections.IDictionaryEnumerator tmpRaceStat = raceStat.GetEnumerator();
            while (tmpRaceStat.MoveNext())	//for each player
            {
                raceStats p = (raceStats)tmpRaceStat.Value;
                sorted.Add(p);
            }
            raceStats.modeSort = (int)sortRaceStats.SORT_RESULT;
            sorted.Sort();

            if(!Directory.Exists("Export"))
                System.IO.Directory.CreateDirectory("Export");

            var raceEngine = new DelimitedFileEngine<infoRace>();
            raceEngine.HeaderText = "datFile,currentTrackName,maxSplit,weather,wind,raceLaps,sraceLaps,qualMins,HName,currLap,isToc";
            var raceResults = new List<infoRace>();
            currInfoRace.datFile = datFile;
            raceResults.Add(currInfoRace);
            raceEngine.WriteFile("Export/" + datFile + "_race.csv", raceResults);



            var engine = new DelimitedFileEngine<raceStats>();
            engine.HeaderText = "datFile,UCID,PLID,userName,nickName,Plate,bestSplit1,lapBestSplit1,bestSplit2,lapBestSplit2,bestSplit3,lapBestSplit3,bestLastSplit,lapBestLastSplit,cumuledTime,bestSpeed,lapBestSpeed,numStop,cumuledStime,resultNum,finalPos,finished,finPLID,totalTime,bestLap,lapBestLap,CName,penalty,gridPos,lapsLead,tmpTime,firstTime,avgTime,curBestSplit,curWrSplit,curLapBestSplit,lapStability,curSplit1,curSplit2,curSplit3,yellowFlags,inYellow,blueFlags,inBlue,sFlags,numPen,lastSplit,CurrIdxSplit";
            
            List<raceStats> results = new List<raceStats>();
            foreach (raceStats r in sorted)
            {
                r.datFile = datFile;
                results.Add(r);
            }

            engine.WriteFile("Export/" + datFile + "_results_race.csv", results);
            
            List<Lap> lapResults = new List<Lap>();
            foreach (raceStats r in sorted)
            {
                int i = 1;
                foreach (Lap lap in r.lap)
                {
                    lap.datFile = datFile;
                    lap.UCID = r.UCID;
                    lap.PLID = r.PLID;
                    lap.lap = i++;
                    lapResults.Add((Lap)lap);
                }
            }

            var engine2 = new DelimitedFileEngine<Lap>();
            engine2.HeaderText = "datFile,UCID,PLID,lap,split1,split2,split3,lapTime,cumuledTime";
            engine2.WriteFile("Export/" + datFile + "_results_race_laps.csv", lapResults); 


            ////this.ContentTypeFilters.Register(ContentType.Csv, CsvSerializer.SerializeToStream, CsvSerializer.DeserializeFromStream);

            ////using (System.IO.StreamWriter sw = new System.IO.StreamWriter("Export/" + datFile + "_results_race.csv"))
            ////{
            //    //int curPos = 0;
            //    for (int i = 0; i < sorted.Count; i++)
            //    {

            //        raceStats p = (raceStats)sorted[i];



            //        //curPos++;
            //        //raceStats p = (raceStats)sorted[i];
            //        //if (i == 0)
            //        //{
            //        //    firstMaxLap = p.lap.Count;
            //        //    firstTotalTime = p.totalTime;
            //        //}
            //        //string resultLine = formatLine;
            //        //resultLine = resultLine.Replace("[RaceResults ", "");
            //        //resultLine = resultLine.Replace("]", "");
            //        //resultLine = resultLine.Replace("{Position}", curPos.ToString());
            //        ////                    resultLine = resultLine.Replace("{Position}", p.resultNum.ToString() );
            //        //resultLine = resultLine.Replace("{PlayerName}", p.nickName.Replace("^0", "").Replace("^1", "").Replace("^2", "").Replace("^3", "").Replace("^4", "").Replace("^5", "").Replace("^6", "").Replace("^7", "").Replace("^8", ""));
            //        //resultLine = resultLine.Replace("{UserName}", p.userName);
            //        //resultLine = resultLine.Replace("{Car}", p.CName);
            //        //// if Racer do not finish
            //        //if (p.resultNum == 999)
            //        //    resultLine = resultLine.Replace("{Gap}", "DNF");
            //        //else
            //        //{
            //        //    if (firstMaxLap == p.lap.Count)
            //        //    {
            //        //        if (i == 0)
            //        //            resultLine = resultLine.Replace("{Gap}", raceStats.LfstimeToString(p.totalTime));
            //        //        else
            //        //        {
            //        //            long tres;
            //        //            tres = p.totalTime - firstTotalTime;
            //        //            resultLine = resultLine.Replace("{Gap}", "+" + raceStats.LfstimeToString(tres));
            //        //        }
            //        //    }
            //        //    else
            //        //        resultLine = resultLine.Replace("{Gap}", "+" + ((int)(firstMaxLap - p.lap.Count)).ToString() + " laps");
            //        //}
            //        //resultLine = resultLine.Replace("{BestLap}", raceStats.LfstimeToString(p.bestLap));
            //        //resultLine = resultLine.Replace("{LapsDone}", p.lap.Count.ToString());
            //        //resultLine = resultLine.Replace("{PitsDone}", p.numStop.ToString());
            //        //resultLine = resultLine.Replace("{Penalty}", p.penalty);
            //        //resultLine = resultLine.Replace("{PosGrid}", p.gridPos.ToString());
            //        //resultLine = resultLine.Replace("{Flags}", p.sFlags);
            //        //sw.WriteLine(resultLine);
            //    }
            ////}
        }




    }
}
