/*
** Implementation of action Close (close) for Notification
** This file is only generated once; you may change it as needed.
** In case you want to re-generate it, simply delete it an re-run the code generator.
** Do not change the method's name or signature as this wil break the calling code.
**
*/
import { ILoadResult, ISaveMessage, IAPIResult } from '../GeneralTypes';
import { http } from '../http';
import { Utils } from '../Utils';
import { INotificationData } from './generated/NotificationTypes';
import {NotificationAPI} from './generated/NotificationAPI';

export class NotificationActionclose {
    static perform = async (id: number, token: string): Promise<ISaveMessage<INotificationData>> => {
        try {
            // get the record from the backend
            var recAPIResult = await NotificationAPI.loadOneRecord(id, token);
            var rec : INotificationData = recAPIResult.dataSubject!;

            // perform action on the record
            // TODO
            // send the updated record back to the caller for saving
            var saveMessage : ISaveMessage<INotificationData> =  { id: id, dataSubject : rec, action: "Save", subaction: "Close", additionalData: [] };

            return saveMessage;
        } catch (ex) {
            console.error(ex);
            throw ex;
        }
    }
}
