using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Models;

namespace DAL
{
    public interface IWonder
    {
        #region Processor

        List<ProcessorVM> GetAllProcessors();

        IEnumerable<ProcessorVM> ProcessorPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetProcessorBrandNamesAndNumbers();

        IEnumerable<ProcessorVM> GetProcessorProductsByPrice(IEnumerable<ProcessorVM> processorVMs, int Id);

        IEnumerable<ProcessorVM> GetProcessorProductsByBrand(string[] BName, int PNumber, int SNumber, int id, int min, int max);

        IEnumerable<ProcessorVM> ProcessorPrice(int min, int max, int PSize, int NPage);
        IEnumerable<ProcessorVM> ProcessorPriceBrand(int PageNumber, int PageSize, int Id, string[] BName);

        IEnumerable<ProcessorVM> ProcessorPaginByBrand(int PNum, int SNum, string[] BName);
        IEnumerable<ProcessorVM> GetProcessorDependentOnSort(int id);

        IEnumerable<ProcessorVM> GetProcessorPriceDependentOnBrand(int min, int max, int sort);

        //IEnumerable<ProcessorVM> GetPriceDependentOnBrand(int min, int max, int sort);



        #endregion

        #region Motherboard

        List<MotherboardVM> GetAllMotherboard();

        IEnumerable<MotherboardVM> MotherboardPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetMotherboardBrandNamesAndNumbers();

        IEnumerable<MotherboardVM> GetMotherboardProductsByPrice(IEnumerable<MotherboardVM> motherboardVM, int Id);

        IEnumerable<MotherboardVM> GetMotherboardProductsByBrand(string[] BName, int PNumber, int SNumber, int id, int min, int max);

        IEnumerable<MotherboardVM> MotherboardPrice(int min, int max, int PSize, int NPage);
        IEnumerable<MotherboardVM> MotherboardPriceBrand(int PageNumber, int PageSize, int Id, string[] BName);

        IEnumerable<MotherboardVM> MotherboardPaginByBrand(int PNum, int SNum, string[] BName);
        IEnumerable<MotherboardVM> GetMotherboardDependentOnSort(int id);
        IEnumerable<MotherboardVM> GetMotherboardPriceDependentOnBrand(int min, int max, int sort);


        #endregion

        #region HDD

        List<HddVM> GetAllHDD();

        IEnumerable<HddVM> HDDPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetHDDBrandNamesAndNumbers();

        IEnumerable<HddVM> GetHDDProductsByPrice(IEnumerable<HddVM> hddVM, int Id);

        IEnumerable<HddVM> GetHDDProductsByBrand(string[] BName, int PNumber, int SNumber, int id, int min, int max);

        IEnumerable<HddVM> HDDPrice(int min, int max, int PSize, int NPage);
        IEnumerable<HddVM> HDDPriceBrand(int PageNumber, int PageSize, int Id, string[] BName);

        IEnumerable<HddVM> HDDPaginByBrand(int PNum, int SNum, string[] BName);
        IEnumerable<HddVM> GetHDDDependentOnSort(int id);
        IEnumerable<HddVM> GetHDDPriceDependentOnBrand(int min, int max, int sort);
        #endregion

        #region RAM

        List<RamVM> GetAllRAM();

        IEnumerable<RamVM> RAMPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetRAMBrandNamesAndNumbers();

        IEnumerable<RamVM> GetRAMProductsByPrice(IEnumerable<RamVM> RamVMs, int Id);

        IEnumerable<RamVM> GetRAMProductsByBrand(string[] BName, int PNumber, int SNumber);

        IEnumerable<RamVM> RAMPrice(int min, int max, int PSize, int NPage);

        #endregion

        #region SSd

        List<SsdVM> GetAllSSD();

        IEnumerable<SsdVM> SSDPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetSSDBrandNamesAndNumbers();

        IEnumerable<SsdVM> GetSSDProductsByPrice(IEnumerable<SsdVM> SsdVMs, int Id);

        IEnumerable<SsdVM> GetSSDProductsByBrand(string[] BName, int PNumber, int SNumber);

        IEnumerable<SsdVM> SSDPrice(int min, int max, int PSize, int NPage);

        #endregion

        #region Graphics Card

        IEnumerable<GraphicsCardVM> GetCardProductsByBrand(string[] BName, int PNumber, int SNumber);

        IEnumerable<GraphicsCardVM> GetCardVMProductsByPrice(IEnumerable<GraphicsCardVM> CardVMVMs, int Id);

        IEnumerable<BrandVM> GetCardVMBrandNamesAndNumbers();

        IEnumerable<GraphicsCardVM> CardPaginations(int PNum, int SNum);

        List<GraphicsCardVM> GetAllCard();

        IEnumerable<GraphicsCardVM> CardPrice(int min, int max, int PSize, int NPage);
        #endregion

        #region Case

        List<CaseVM> GetAllCase(string deleteddata = null);

        IEnumerable<CaseVM> CasePaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetCaseVMBrandNamesAndNumbers();

        IEnumerable<CaseVM> GetCaseVMProductsByPrice(IEnumerable<CaseVM> caseVMs, int Id);

        IEnumerable<CaseVM> GetCaseProductsByBrand(string[] BName, int PNumber, int SNumber);

        IEnumerable<CaseVM> CasePrice(int min, int max, int PSize, int NPage);
        #endregion

        #region PowerSuply

        List<PowerSupplyVM> GetAllPowerSuply();

        IEnumerable<PowerSupplyVM> PowerSuplyPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetPowerSupplyBrandNamesAndNumbers();

        IEnumerable<PowerSupplyVM> GetPowerSupplyProductsByPrice(IEnumerable<PowerSupplyVM> PowerSupplyVMs, int Id);

        IEnumerable<PowerSupplyVM> GetPowerSupplyVMsProductsByBrand(string[] BName, int PNumber, int SNumber);

        IEnumerable<PowerSupplyVM> PSPrice(int min, int max, int PSize, int NPage);
        #endregion

        #region Product Details

        CaseVM CaseDetails(string code);

        GraphicsCardVM GraphicsCardDetails(string code);

        HddVM HddDetails(string code);

        MotherboardVM MotherboardDetails(string code);

        PowerSupplyVM PowerSupplyDetails(string code);

        ProcessorVM ProcessorDetails(string code);

        RamVM RamDetails(string code);

        SsdVM SsdDetails(string code);

        #endregion

        #region comments Pagination 
        CaseVM CaseCommentsPagination(string code, int currentPageIndex);

        #endregion

        #region get Data Except one
        //List<MotherboardVM> GetTopMothers();
        List<MotherboardVM> GetMotherBoardsExceptOne(string code);

        List<ProcessorVM> GetProcessorsExceptOne(string code);

        List<RamVM> GetRamExceptOne(string code);

        List<GraphicsCardVM> GetVGAExceptOne(string code);

        List<HddVM> GetHDDExceptOne(string code);

        List<SsdVM> GetSSDExceptOne(string code);

        List<PowerSupplyVM> GetPSUExceptOne(string code);

        List<CaseVM> GetCaseExceptOne(string code);

        #endregion

        #region Get New Data
        List<MotherboardVM> GetNewMotherBoards();

        List<ProcessorVM> GetNewProcessors();

        List<RamVM> GetNewRam();

        List<GraphicsCardVM> GetNewVGA();

        List<HddVM> GetNewHDD();

        List<SsdVM> GetNewSSD();

        List<PowerSupplyVM> GetNewPSU();

        List<CaseVM> GetNewCase();

        #endregion

        #region Get Top Data

        public List<MotherboardVM> GetTopMothers();

        #endregion

        #region Check Order
        string CheckOrderCreateAcc(UserVM UserData, SalesVM[] OrderData);

        string CheckOrderSignIn(UserVM UserData, SalesVM[] OrderData);

        string CheckOrder(SalesVM[] OrderData);

        #endregion

        #region WishList

        List<WishListVM> GetWishList(int userid);
        string DeletefromWL(string ProductCode, int userid);
        string CheckfromWL(string ProductCode, int userid);
        string AddToWL(string ProductCode, int userid);

        #endregion

        #region Review
        public ReviewVM AddReview(ReviewVM review);
        public List<ReviewVM> Reviews(string code);
        #endregion

        #region Search

        List<Search> SearchProduct(string src);

        List<Search> SearchMotherBoard(string src);

        List<Search> SearchProcessor(string src);

        List<Search> SearchRam(string src);

        List<Search> SearchSSD(string src);

        List<Search> SearchHDD(string src);

        List<Search> SearchCase(string src);

        List<Search> SearchPowerSupply(string src);

        List<Search> SearchVGA(string src);

        #endregion

        public List<Brand> GetProductBrand();
        public List<string> GetBrandNames();

        #region Admin Project

        public List<UserVM> GetUsersData();

        public List<UserVM> GetAdmins();

        public List<SalesVM> GetProcessor();
        public List<SalesVM> GetMotherboard();
        public List<SalesVM> GetSDD();
        public List<SalesVM> GetHDD();
        public List<SalesVM> GetCases();
        public List<SalesVM> GetPowerSupplies();
        public List<SalesVM> GetGraphicsCard();
        public List<SalesVM> GetRam();











        #endregion
    }
}