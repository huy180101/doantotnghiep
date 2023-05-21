import { statusCode } from "@/common/enum";
import {
  fetcher_$GET,
  fetcher_$POST,
  formatDate,
  notificationError,
  notificationSuccess,
} from "@/common/functionglobal";
import { classSV } from "@/services/class";
import { student } from "@/services/student";
import { EditOutlined } from "@ant-design/icons";
import { Form, Input, Modal, Space, Tooltip,Table } from "antd";
import { ColumnsType } from "antd/es/table";


import { useRouter } from "next/router";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import useSWR from "swr";

const { Item } = Form;

type Props = {
  show: boolean;
  handleCompositeScoreModalClose: any;
  data: any;
};
export interface DataType {
  birthday: string,
  class_id: string,
  email: string,
  full_name: string,
  id: number,
  is_delete: boolean,
  is_officer: boolean,
  pass_code: string,
  password: string,
  student_code: string,
  teacher_id: number,
  username: string,
  total_point: number,
  sort: string,
}

const CompositeScoreModal: React.FC<Props> = (props) => {
  const router = useRouter();

  const columns: ColumnsType<DataType> = [
    {
      title: "STT",
      render: (_, record, index) => (
        <div className="center">{index + 1}</div>
      ),
    },
    {
      title: "Mã HSSV",
      dataIndex: "student_code",
      key: "student_code",
    },
    {
      title: "Họ và tên",
      dataIndex: "full_name",
      key: "full_name",
    },
    {
      title: "Ngày sinh",
      dataIndex: "birthday",
      render: (_, record, index) => (
        <div>
          {record.birthday}
        </div>
      ),
    },
    {
      title: "Tổng điểm",
      dataIndex: "total_point",
      key: "total_point",
    },
    {
      title: "Xếp loại",
      dataIndex: "sort",
      key: "sort",
    },
    {
      title: "Thao tác",
      key: "action",
      render: (_, record, index) => (
        <Space size="middle">
          <Tooltip placement="topLeft" title='Cập nhật'>
            <EditOutlined
              onClick={() => router.push(`/banghanhkiem/${record.id}`)}
            />
          </Tooltip>
        </Space>
      ),
    },
  ];

  const [lstTable, setListTable] = useState();
  const [lstStudent, setListStudent] = useState([]);
  const [compositeScore, setCompositeScore] = useState<any>();

  const getInfoCurrentUser = useSelector(
    (state: any) => state.infoCurrentUserReducers
  );

  const {
    data: dataStudent,
    error: errorStudent,
  } = useSWR(((props?.show) && (props.data?.id)) ? `${student().list(props.data.id)}` : null, fetcher_$GET);

  const {
    data: listRes,
    error,
    isLoading,
    mutate,
  } = useSWR((dataStudent?.data) ? `${classSV().detail(props.data.teacher_id)}` : null, fetcher_$GET);

  useEffect(() => {
    if (!!dataStudent && !errorStudent) {
      let dataTalbe = dataStudent.data;

      (async () => {
        dataTalbe?.map((obj: any, index: number) => {
          obj.key = index + 1;
          obj.birthday = formatDate(obj.birthday);
          return obj
        });
        setListStudent(await dataTalbe);
      })();
    }
    if (!!listRes && !error) {
      let dataTalbe = dataStudent?.data;

      (async () => {

        dataTalbe.map((obj: any, index: number) => {
          obj.total_point = listRes.data.conduct_Forms[index].total_point;
          if (obj.total_point >= 90) {
            obj.sort = 'Xuất sắc';
          }
          else if (obj.total_point >= 80) {
            obj.sort = 'Tốt';
          }
          else if (obj.total_point >= 65) {
            obj.sort = 'Khá';
          }
          else if (obj.total_point >= 50) {
            obj.sort = 'Trung bình';
          }
          else if (obj.total_point >= 35) {
            obj.sort = 'Yếu kém';
          }
        });
        setListTable(await dataTalbe);
      })();
    }
  }, [error, listRes, dataStudent, errorStudent]);

  return (
    <>
      <Modal
        title={"Bảng điểm tổng hợp"}
        centered
        open={props.show}
        // onOk={() => {
        //   form.submit();
        // }}
        // okButtonProps={{
        //   className: "bg-blue-500",
        // }}
        // okText="Xác nhận"
        cancelText="Thoát"
        onCancel={props.handleCompositeScoreModalClose}
        width={1500}
        okButtonProps={{ style: { display: 'none' } }}
      >
        <Table columns={columns} dataSource={lstTable ?? []} />
      </Modal>
    </>
  );
};

export default CompositeScoreModal;