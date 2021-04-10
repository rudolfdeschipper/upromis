/*
** Implementation of action Close (close) for Client
** This file is only generated once; you may change it as needed.
** In case you want to re-generate it, simply delete it an re-run the code generator.
** Do not change the method's name or signature as this wil break the calling code.
**
*/
import { ILoadResult, ISaveMessage, IAPIResult } from '../GeneralTypes';
import { http } from '../http';
import { Utils } from '../Utils';
import { IClientData } from './generated/ClientTypes';
import {ClientAPI} from './generated/ClientAPI';

export class ClientActionclose {
    static perform = async (id: number, token: string): Promise<ISaveMessage<IClientData>> => {
        try {
            // get the record from the backend
            var recAPIResult = await ClientAPI.loadOneRecord(id, token);
            var rec : IClientData = recAPIResult.dataSubject!;

            // perform action on the record
            // TODO
            // send the updated record back to the caller for saving
            var saveMessage : ISaveMessage<IClientData> =  { id: id, dataSubject : rec, action: "Save", subaction: "Close", additionalData: [] };

            return saveMessage;
        } catch (ex) {
            console.error(ex);
            throw ex;
        }
    }
}
