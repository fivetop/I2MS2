using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using System.Runtime.InteropServices;

// GS_DEL 2016.06.30 
// DLL 모듈로 되있는걸 임포트 시킴 
// 

namespace WebApiClient
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Handlers;
    using System.IO;
    using System.Net;
    using System.Threading;
    using I2MS2.Models;

    public class WebApiClientClass
    {
        private CancellationTokenSource cts;
        public int percentage;
        private string web_api_uri_string;
        private bool firsttime = false;

        // 요청한 처리를 취소 시킨다.
        public void cancel()
        {
            cts.Cancel();
        }

        // 생성자에서 연결할 URL을 지정한다.
        public WebApiClientClass(string web_api_uri)
        {
            web_api_uri_string = web_api_uri;
        }

        public WebApiClientClass()
        {
        }

        public void set_server(string web_api_uri)
        {
            web_api_uri_string = web_api_uri;
        }

        // 서버에 위치한 이미지 파일을 삭제한다.
        //----------------------------------------------------------------------
        // sub_dir : 이미지가 위치한 서브 디렉토리명
        // file_name : 서버 이미지 명 (file명만)
        // 결과 : 0=성공, 1=실패
        // ex) sub_dir="man", file_name="BodyPart-..................._abc.jpg"

        public async Task<int> deleteFile(string sub_dir, string file_name)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response;

                client.BaseAddress = new Uri(web_api_uri_string);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET (화일 삭제)
                try
                {
                    response = await client.GetAsync("api/delete?file_name=" + sub_dir + "/" + file_name, HttpCompletionOption.ResponseHeadersRead);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("File Deleted. sub_dir={0}, file_name={1}", sub_dir, file_name);
                        return 0;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error={0}, Message={1}", e.HResult, e.Message);
                    return 1;
                }

                Console.WriteLine("Not Found");
                return 1;
            }
        }

        // 서버에 이미지 파일을 업로드한다.
        //----------------------------------------------------------------------
        // sub_dir : 이미지가 위치한 서브 디렉토리명
        // file_name : 서버 이미지 명 (file명만)
        // buffer_size : 버퍼 사이즈 (보통 1024 이상)
        // 결과 : TransferResult 클래스로 리턴
        //            TransferResult.server_file_name : 성공 시 서버에 저장된 화일명이 담김, 
        //                            ex) "BodyPart.................._abc.jpg"
        //            TransferResult.result_code      : 0=성공, 1=실패(또는 작업 취소)

        public async Task<TransferResult> uploadFile(string sub_dir, string file_name, int buffer_size)
        {
            cts = new CancellationTokenSource();

            // Create a progress notification handler
            ProgressMessageHandler progress = new ProgressMessageHandler();
            progress.HttpSendProgress += ProgressUploadEventHandler;

            // Create an HttpClient and wire up the progress handler
            HttpClient client = HttpClientFactory.Create(progress);

            // Set the request timeout as large uploads can take longer than the default 20 minute timeout
            client.Timeout = TimeSpan.FromMinutes(1);

            try
            {
                // Open the file we want to upload and submit it
                using (FileStream fileStream = new FileStream(file_name, FileMode.Open, FileAccess.Read, FileShare.Read, buffer_size, useAsync: true))
                {
                    // Create a stream content for the file
                    StreamContent content = new StreamContent(fileStream, buffer_size);

                    // Create Multipart form data content, add our submitter data and our stream content
                    MultipartFormDataContent formData = new MultipartFormDataContent();
                    formData.Add(new StringContent("Me"), "submitter");
                    formData.Add(content, "filename", file_name);

                    // Post the MIME multipart form data upload with the file
                    Uri address = new Uri(web_api_uri_string + "api/upload?sub_dir=" + sub_dir);
                    HttpResponseMessage response = await client.PostAsync(address, formData).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Console.WriteLine("Error. code={0}, message={1}", response.StatusCode, response.ReasonPhrase);
                        return new TransferResult { server_file_name = "", result_code = 1 };
                    }

                    return await response.Content.ReadAsAsync<TransferResult>(cts.Token).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error={0}, Message={1}", e.HResult, e.Message);
                return new TransferResult { server_file_name = "", result_code = 1 };
            }
        }

        // 서버로부터 이미지 파일을 다운로드한다.
        //----------------------------------------------------------------------
        // sub_dir : 이미지가 위치한 서브 디렉토리명
        // file_name : 서버 이미지 파일명 (file명만)
        // dest_dir : 클라이언트 PC에 보관될 이미지 파일 디렉토리 위치 명(full path)
        // buffer_size : 버퍼 사이즈 (보통 1024 이상)
        // 리턴: 0=성공, 1=실패(또는 작업 취소)
        public async Task<int> downloadFile(string sub_dir, string file_name, string dest_dir, int buffer_size)
        {
            cts = new CancellationTokenSource();

            string _address = web_api_uri_string + "api/download?file_name=" + sub_dir + "/" + file_name;
            int cnt = 0;
            Console.WriteLine("File downloading...");

            // Create a progress notification handler
            ProgressMessageHandler progress = new ProgressMessageHandler();
            progress.HttpReceiveProgress += ProgressDownloadEventHandler;

            try
            {
                // using (var client = new HttpClient())
                using (var client = HttpClientFactory.Create(progress))
                {
                    client.Timeout = TimeSpan.FromMinutes(20);

                    // Send asynchronous request
                    HttpResponseMessage response = await client.GetAsync(_address, HttpCompletionOption.ResponseHeadersRead);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Console.WriteLine("File download canceled.");
                        return 1;
                    }

                    string file = dest_dir + file_name;

                    using (var fileStream = File.Create(file))
                    {
                        using (var httpStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            // CopyTo() 명령과 같이 지정된 메모리 크기로 하지 않으려면 아래와 같이 처리한다.
                            //-----------------------------------------------------------------
                            //httpStream.CopyTo(fileStream);

                            //System.Threading.CancellationToken ct;
                            byte[] buffer = new byte[buffer_size];
                            while (true)
                            {
                                int read = await httpStream.ReadAsync(buffer, 0, buffer.Length, cts.Token).ConfigureAwait(false);
                                cnt += read;

                                if (read <= 0)
                                    break;

                                // do the policing here

                                await fileStream.WriteAsync(buffer, 0, read);
                            }
                            fileStream.Flush();
                        }
                    }
                }
                Console.WriteLine("File download completed. length={0}", cnt);
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error={0}, Message={1}", e.HResult, e.Message);
                return 1;
            }
        }
        private void ProgressUploadEventHandler(object sender, HttpProgressEventArgs eventArgs)
        {
            // The sender is the originating HTTP request   
            HttpRequestMessage request = sender as HttpRequestMessage;

            // Write different message depending on whether we have a total length or not   
            string message;
            if (eventArgs.TotalBytes != null)
            {
                message = String.Format("  Request {0} uploaded {1} of {2} bytes ({3}%)",
                    request.RequestUri, eventArgs.BytesTransferred, eventArgs.TotalBytes, eventArgs.ProgressPercentage);
                percentage = eventArgs.ProgressPercentage;
            }
            else
            {
                message = String.Format("  Request {0} uploaded {1} bytes",
                    request.RequestUri, eventArgs.BytesTransferred);
            }


            // Write progress message to console   
            Console.WriteLine(message);
        }

        private void ProgressDownloadEventHandler(object sender, HttpProgressEventArgs eventArgs)
        {
            // The sender is the originating HTTP request   
            HttpRequestMessage request = sender as HttpRequestMessage;

            // Write different message depending on whether we have a total length or not   
            string message;
            if (eventArgs.TotalBytes != null)
            {
                message = String.Format("  Request {0} downloaded {1} of {2} bytes ({3}%)",
                    request.RequestUri, eventArgs.BytesTransferred, eventArgs.TotalBytes, eventArgs.ProgressPercentage);
                percentage = eventArgs.ProgressPercentage;
            }
            else
            {
                message = String.Format("  Request {0} downloaded {1} bytes",
                    request.RequestUri, eventArgs.BytesTransferred);
            }

            // Write progress message to console   
            Console.WriteLine(message);
        }

        // 테이블 내용을 모두 조회한다.
        //---------------------------------------------------------
        // table : DB 테이블명
        // type  : 리턴할 변수 영역 타입
        // 결과  : Task<object> 형태로 리턴 --> 결국 List<T> 형태로 읽어 낼 수 있음
        // ex) List<region1> list = (List<region1>) webapi.getList("region1", typeof(List<region1>));
        //public async Task<object> getList(string table, Type type)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        HttpResponseMessage response;

        //        client.BaseAddress = new Uri(web_api_uri_string);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        // 1. HTTP GET (지역1 목록 조회)
        //        string query = string.Format("api/" + table);

        //        try
        //        {
        //            response = await client.GetAsync(query).ConfigureAwait(false);
        //            response.EnsureSuccessStatusCode();
        //            return await response.Content.ReadAsAsync(type).ConfigureAwait(false);
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine("Error={0}, Message={1}", e.HResult, e.Message);
        //            return null;
        //        }
        //    }
        //}


        // 테이블 내용을 모두 조회한다.
        //---------------------------------------------------------
        // table : DB 테이블명
        // type  : 리턴할 변수 영역 타입
        // 결과  : Task<object> 형태로 리턴 --> 결국 List<T> 형태로 읽어 낼 수 있음
        // ex) List<region1> list = (List<region1>) webapi.getList("region1", typeof(List<region1>));
        public async Task<object> getList(string table, Type type, string filter = "")
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response;

                client.BaseAddress = new Uri(web_api_uri_string);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                // GS_DEL 로긴 정보 송부 
                //client.DefaultRequestHeaders.Add("User", g.login_user_id.ToString());

                // 1. HTTP GET (지역1 목록 조회)
                string query = string.Format("api/{0}{1}", table, filter);

                try
                {
                    response = await client.GetAsync(query).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    if(!firsttime)
                    {
                        // GS_DEL
                        var aa = response.Headers.GetValues("Date1");

                        //DateTimeOffset? offset = response.Headers.Date;
                        //DateTime aa1 = offset.HasValue ? offset.Value.DateTime : DateTime.MaxValue;
                        IFormatProvider KR_Format = new System.Globalization.CultureInfo("ko-KR", true);
                        //g._seever_time = DateTime.Parse(aa.First(),"yyyy-MM-dd-HH-mm-ss" );
                        g._seever_time = DateTime.ParseExact(aa.First(), "yyyy-MM-dd-HH-mm-ss", KR_Format);

                        DateTimeTest.SetSystemDateTime(g._seever_time);
                        firsttime = true;
                    }

                    return await response.Content.ReadAsAsync(type).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    g._seever_time = DateTime.Now;
                    Console.WriteLine("Error={0}, Message={1}", e.HResult, e.Message);
                    return null;
                }
            }
        }

        // 테이블 내용을 ID로 1건 조회한다.
        //---------------------------------------------------------
        // table : DB 테이블명
        // id    : id 번호
        // type  : 리턴할 변수 영역 타입
        // 결과  : Task<object> 형태로 리턴 --> 결국 클래스 형태로 읽어 낼 수 있음
        // ex) region1 r1 = (region1) webapi.get("region1", 70190001, typeof(region1));

        public async Task<object> get(string table, int id, Type type)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response;

                client.BaseAddress = new Uri(web_api_uri_string);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string query = string.Format("api/{0}/{1}", table, id) ;
                
                try
                {
                    response = await client.GetAsync(query).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync(type).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error={0}, Message={1}", e.HResult, e.Message);
                    return null;
                }
            }
        }

        // 테이블에 레코드 1건을 추가한다.
        //---------------------------------------------------------
        // table : DB 테이블명
        // o     : 입력할 클래스 변수
        // type  : 리턴할 변수 영역 타입
        // 결과  : Task<object> 형태로 리턴 --> 결국 클래스 형태로 읽어 낼 수 있음
        // ex) region1 r1 = (region1) webapi.post("region1", region1_data, typeof(region1));

        public async Task<object> post(string table, object o, Type type)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response;

                client.BaseAddress = new Uri(web_api_uri_string);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string query = string.Format("api/{0}", table);

                try
                {
                    response = await client.PostAsJsonAsync(query, o).ConfigureAwait(false);
                    var rr = await response.Content.ReadAsAsync(type);
                    return rr;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error={0}, Message={1}", e.HResult, e.Message);
                    return null;
                }
            }
        }

        // 테이블에서 레코드 1건을 수정한다.
        //---------------------------------------------------------
        // table : DB 테이블명
        // id    : id 번호
        // o     : 수정할 클래스 변수 내용
        // type  : 리턴할 변수 영역 타입
        // 결과  : Task<int> 형태로 리턴 --> int 값으로 읽어 낼 수 있음
        //                                   0=성공, 1=실패
        // ex) int rc = webapi.put("region1", 79100001, region1_data, typeof(region1));

        public async Task<int> put(string table, int id, object o, Type type)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response;

                client.BaseAddress = new Uri(web_api_uri_string);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string query = string.Format("api/{0}/{1}", table, id);

                try
                {
                    response = await client.PutAsJsonAsync(query, o).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync<int>().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error={0}, Message={1}", e.HResult, e.Message);
                    return 1;
                }
            }
        }

        // 테이블에서 지정된 id의 레코드 1건을 삭제한다.
        //---------------------------------------------------------
        // table : DB 테이블명
        // id    : id 번호
        // 결과  : Task<int> 형태로 리턴 --> int 값으로 읽어 낼 수 있음
        //                                   0=성공, 1=실패
        // ex) int rc = webapi.delete("region1", 79100001);

        public async Task<int> delete(string table, int id, string filter = "")
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response;

                client.BaseAddress = new Uri(web_api_uri_string);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string query = string.Format("api/{0}/{1}", table, id);

                try
                {
                    response = await client.DeleteAsync(query).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync<int>().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error={0}, Message={1}", e.HResult, e.Message);
                    return 1;
                }
            }
        }

        // 테이블에서 지정된 필터의 레코드들을 검색하여 모두 삭제한다.
        //---------------------------------------------------------
        // table : DB 테이블명
        // 결과  : Task<int> 형태로 리턴 --> int 값으로 읽어 낼 수 있음
        //                                   0=성공, 1=실패
        // ex) int rc = webapi.delete("region1", "?template_id=1");

        public async Task<int> delete(string table, string filter = "")
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response;

                client.BaseAddress = new Uri(web_api_uri_string);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string query = string.Format("api/{0}{1}", table, filter);

                try
                {
                    response = await client.DeleteAsync(query).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync<int>().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error={0}, Message={1}", e.HResult, e.Message);
                    return 1;
                }
            }
        }


        // 테이블에서 레코드 1건을 수정한다.
        //---------------------------------------------------------
        // table : DB 테이블명
        // id    : id 번호
        // o     : 수정할 클래스 변수 내용
        // type  : 리턴할 변수 영역 타입
        // 결과  : Task<int> 형태로 리턴 --> int 값으로 읽어 낼 수 있음
        //                                   0=성공, 1=실패
        // ex) int rc = webapi.put("rack_config", 89400001, 1, "S", rack_config_data, typeof(rack_config));

        public async Task<int> put(string table, int id, int id2, string etc, object o, Type type)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response;

                client.BaseAddress = new Uri(web_api_uri_string);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string query = string.Format("api/{0}/{1}/{2}/{3}", table, id, id2, etc);

                try
                {
                    response = await client.PutAsJsonAsync(query, o).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync<int>().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error={0}, Message={1}", e.HResult, e.Message);
                    return 1;
                }
            }
        }


        // 테이블에서 지정된 id의 레코드 1건을 삭제한다.
        //---------------------------------------------------------
        // table : DB 테이블명
        // id    : id 번호
        // 결과  : Task<int> 형태로 리턴 --> int 값으로 읽어 낼 수 있음
        //                                   0=성공, 1=실패
        // ex) int rc = webapi.delete("rack_config", 89400001, 1, "S", rack_config_data, typeof(rack_config));

        public async Task<int> delete(string table, int id, int id2, string etc, object o, Type type)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response;

                client.BaseAddress = new Uri(web_api_uri_string);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string query = string.Format("api/{0}/{1}/{2}/{3}", table, id, id2, etc);

                try
                {
                    response = await client.DeleteAsync(query).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync<int>().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error={0}, Message={1}", e.HResult, e.Message);
                    return 1;
                }
            }
        }

    }

    // 시스템 날짜/시간 변경하기
    // SetLocalTime은 UTC+0(시)을,
    // SetSystemTime은 UTC+9(시)(표준시간대가 "서울"로 설정된 경우)을 설정한다.
    public class DateTimeTest
    {
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }
        [DllImport("kernel32.dll")]
        public static extern bool SetLocalTime(ref SYSTEMTIME time);
        /// <summary>시스템 날짜/시간을 설정한다.</summary>
        /// <param name="dtNew">설정한 Date/Time</param>
        /// <returns>오류가 없는 경우 true를 응답하며,
        /// 그렇지 않은 경우 false를 응답한다.</returns>
        public static bool SetSystemDateTime(DateTime dtNew)
        {
            bool bRtv = false;
            if (dtNew != DateTime.MinValue)
            {
                SYSTEMTIME st;
                st.wYear = (ushort)dtNew.Year;
                st.wMonth = (ushort)dtNew.Month;
                st.wDayOfWeek = (ushort)dtNew.DayOfWeek;    // Set명령일 경우 이 값은 무시된다.
                st.wDay = (ushort)dtNew.Day;
                st.wHour = (ushort)dtNew.Hour;
                st.wMinute = (ushort)dtNew.Minute;
                st.wSecond = (ushort)dtNew.Second;
                st.wMilliseconds = (ushort)dtNew.Millisecond;
                bRtv = SetLocalTime(ref st); ;    // UTC+0 시간을 설정한다.
                // bRtv = YtnWin32.SetSystemTime(ref st);  // UTC + 표준시간대(대한민궁의 경우 UTC+9)를 설정한다.
            }
            return bRtv;
        }
        public DateTimeTest()
        {
            // 시간 변경이 가능(관리자 권한)한 지 검사한다.
            if (SetSystemDateTime(DateTime.Now))
            {
                Console.WriteLine("시스템 시간 변경 권한을 가진 사용자입니다.");
            }
            else
            {
                Console.WriteLine("시스템 시간 변경 권한이 없습니다. 관리자 권한으로 시작하십시오.");
            }
        }
    }

}
