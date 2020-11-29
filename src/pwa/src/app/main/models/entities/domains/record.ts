import {RecordType} from './record-type';
import {Result} from './result';

export class Record {
  id?: string;
  modifiedOn?: Date;

  prefix?: string;
  expectedValue?: string;
  expectedTimeToLive?: string;
  recordType: RecordType = RecordType.A;

  results: Result[] = [];

  get prettyRecordType(): string {
    return RecordType[this.recordType];
  }

  constructor(record: Record | null = null) {
    if (record) {
      Object.assign(this, record);
      if (record.results) {
        this.results = record.results.map(e => new Result(e));
      }
    }
  }
}
