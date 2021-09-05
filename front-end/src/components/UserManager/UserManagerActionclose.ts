/*
** Implementation of action Close (close) for UserManager
** This file is only generated once; you may change it as needed.
** In case you want to re-generate it, simply delete it an re-run the code generator.
** Do not change the method's name or signature as this wil break the calling code.
**
*/
import { ILoadResult, ISaveMessage, IAPIResult } from '../GeneralTypes';
import { http } from '../http';
import { Utils } from '../Utils';
import { IUserManagerData } from './generated/UserManagerTypes';
import {UserManagerAPI} from './generated/UserManagerAPI';

export class UserManagerActionclose {
    static perform = async (code: string, token: string): Promise<ISaveMessage<IUserManagerData, string>> => {
        try {
            // get the record from the backend
            var recAPIResult = await UserManagerAPI.loadOneRecord(code, token);
            var rec : IUserManagerData = recAPIResult.dataSubject!;

            // perform action on the record
            // TODO
            // send the updated record back to the caller for saving
            var saveMessage : ISaveMessage<IUserManagerData,string> =  { id: code, dataSubject : rec, action: "Save", subaction: "Close", additionalData: [] };

            return saveMessage;
        } catch (ex) {
            console.error(ex);
            throw ex;
        }
    }
}
