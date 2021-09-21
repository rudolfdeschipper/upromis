/*
** Implementation of action Copy (copy) for Activity
** This file is only generated once; you may change it as needed.
** In case you want to re-generate it, simply delete it an re-run the code generator.
** Do not change the method's name or signature as this wil break the calling code.
**
*/
import { ILoadResult, ISaveMessage, IAPIResult } from '../GeneralTypes';
import { http } from '../http';
import { Utils } from '../Utils';
import { IActivityData } from './generated/ActivityTypes';
import {ActivityAPI} from './generated/ActivityAPI';

export class ActivityActioncopy {
    static perform = async (id: number, token: string): Promise<ISaveMessage<IActivityData>> => {
        try {
            // get the record from the backend
            var recAPIResult = await ActivityAPI.loadOneRecord(id, token);
            var rec : IActivityData = recAPIResult.dataSubject!;

            // perform action on the record
            // TODO
            // send the updated record back to the caller for saving
            var saveMessage : ISaveMessage<IActivityData> =  { id: id, dataSubject : rec, action: "Save", subaction: "Copy", additionalData: [] };

            return saveMessage;
        } catch (ex) {
            console.error(ex);
            throw ex;
        }
    }
}
