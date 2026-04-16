# (C# 코딩) 파일 비교 툴

## 개요

- **C# 프로그래밍 학습**
- **1줄 소개**:
    - 폴더 브라우저를 통해 경로를 선택하고, 파일 목록을 비교하여 동기화할 수 있는 관리 도구입니다.
- **사용한 플랫폼**: 
    - C#, .NET Windows Forms, Visual Studio 2022, GitHub
- **사용한 컨트롤**: 
    - `SplitContainer`, `Label`, `TextBox`, `ListView`, `Button`, `FolderBrowserDialog`
- **사용한 기술과 구현한 기능**:
    - **UI 디자인**: `SplitContainer`를 활용한 좌우 분할 화면 구성
    - **폴더 선택**: `FolderBrowserDialog`를 이용한 로컬 디렉터리 경로 탐색 및 획득
    - **경로 표시**: 선택된 경로를 `TextBox`에 실시간으로 업데이트
    - **ListView 상세 표시(목록 헤더)**: 좌/우 `ListView`에 "이름", "수정일", "크기" 컬럼을 추가하고 `View = View.Details`로 설정하여 헤더와 각 항목의 컬럼이 보이도록 구현했습니다. (`FullRowSelect`, `GridLines` 설정 포함)
    - **항목 자동 채우기**: 폴더 선택 시 파일 목록을 자동으로 읽어와 항목을 채우는 `PopulateListView(ListView, string)` 헬퍼 메서드를 추가했습니다. 각 항목은 파일명(Name), 최종수정일(LastWriteTime, yyyy-MM-dd HH:mm:ss), 파일크기(Bytes)를 SubItem으로 표시합니다.

## 실행 화면 (과제1)

-과제1 코드의 실행 스크린샷

![과제1 실행화면](img/compare1-1.png)


- **구현한 내용**:
    - UI 디자인 및 배치: SplitContainer를 중심으로 좌우 대칭형 구조를 설계하고, Label, Button, TextBox, ListView 등 주요 컨트롤을 가이드에 맞춰 배치했습니다.
     
    - 컨트롤 명명 규칙 준수: 각 컨트롤의 역할을 명확히 알 수 있도록 txtLeftDir, btnLeftDir, lvwLeftDir 등 지정된 변수명을 부여했습니다.
     
    - 폴더 선택 기본 로직: FolderBrowserDialog를 호출하여 사용자가 비교할 대상 폴더를 로컬 디렉터리에서 탐색하고 선택할 수 있는 기능을 구현했습니다.

    - **UI 구성**: `SplitContainer`를 활용하여 좌측 폴더와 우측 폴더의 정보를 독립적으로 보여줄 수 있는 영역을 확보하고 시각적으로 그룹화했습니다.
     
    
     
## 실행 화면 (과제2)
- **코드의 실행 스크린샷과 구현 내용 설명**
![실행화면](img/screenshot-2.png)
- (과제 2 진행 후 내용을 업데이트하세요.)

## 실행 화면 (과제3)
- **코드의 실행 스크린샷과 구현 내용 설명**
![실행화면](img/screenshot-3.png)
- (과제 3 진행 후 내용을 업데이트하세요.)

## 실행 화면 (과제4)
- **코드의 실행 스크린샷과 구현 내용 설명**
![실행화면](img/screenshot-4.png)
- (과제 4 진행 후 내용을 업데이트하세요.)

