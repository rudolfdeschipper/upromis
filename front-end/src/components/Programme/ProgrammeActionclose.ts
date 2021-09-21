/*
** Implementation of action Close (close) for Programme
** This file is only generated once; you may change it as needed.
** In case you want to re-generate it, simply delete it an re-run the code generator.
** Do not change the method's name or signature as this wil break the calling code.
**
*/
import { ILoadResult, ISaveMessage, IAPIResult } from '../GeneralTypes';
import { http } from '../http';
import { Utils } from '../Utils';
import { IProgrammeData } from './generated/ProgrammeTypes';
import {ProgrammeAPI} from './generated/ProgrammeAPI';

export class ProgrammeActionclose {
    static perform = async (id: number, token: string): Promise<ISaveMessage<IProgrammeData>> => {
        try {
            // get the record from the backend
            var recAPIResult = await ProgrammeAPI.loadOneRecord(id, token);
            var rec : IProgrammeData = recAPIResult.dataSubject!;

            // perform action on the record
            // TODO
            // send the updated record back to the caller for saving
            var saveMessage : ISaveMessage<IProgrammeData> =  { id: id, dataSubject : rec, action: "Save", subaction: "Close", additionalData: [] };

            return saveMessage;
        } catch (ex) {
            console.error(ex);
            throw ex;
        }
    }
}
