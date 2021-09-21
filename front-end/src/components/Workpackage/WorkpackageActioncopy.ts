/*
** Implementation of action Copy (copy) for Workpackage
** This file is only generated once; you may change it as needed.
** In case you want to re-generate it, simply delete it an re-run the code generator.
** Do not change the method's name or signature as this wil break the calling code.
**
*/
import { ILoadResult, ISaveMessage, IAPIResult } from '../GeneralTypes';
import { http } from '../http';
import { Utils } from '../Utils';
import { IWorkpackageData } from './generated/WorkpackageTypes';
import {WorkpackageAPI} from './generated/WorkpackageAPI';

export class WorkpackageActioncopy {
    static perform = async (id: number, token: string): Promise<ISaveMessage<IWorkpackageData>> => {
        try {
            // get the record from the backend
            var recAPIResult = await WorkpackageAPI.loadOneRecord(id, token);
            var rec : IWorkpackageData = recAPIResult.dataSubject!;

            // perform action on the record
            // TODO
            // send the updated record back to the caller for saving
            var saveMessage : ISaveMessage<IWorkpackageData> =  { id: id, dataSubject : rec, action: "Save", subaction: "Copy", additionalData: [] };

            return saveMessage;
        } catch (ex) {
            console.error(ex);
            throw ex;
        }
    }
}
